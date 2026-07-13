import {getJsonMappingsList, getMappings, mapJsonStrings, mapJsonValues, saveMappings} from "./api/mappingApi.ts";
import {useEffect, useState} from "react";
import MappingTable from "./components/MappingTable";
import type {JsonMappingSummary, Mapping, MappingResponse} from "./types/mapping.ts";
import MappingList from "./components/MappingList.tsx";

function App() {

    const [firstJson, setFirstJson] = useState("");
    const [secondJson, setSecondJson] = useState("");

    const [result, setResult] = useState<MappingResponse | null>(null);

    const [mappings, setMappings] = useState<Mapping[]>([]);
    const [targetOptions, setTargetOptions] = useState<string[]>([]);

    const [savedMappings, setSavedMappings] = useState<JsonMappingSummary[]>([]);
    
    const [mappedJson, setMappedJson] = useState("");
    
    useEffect(() => {
        handleLoadSavedMappings();
    }, []);
    
    const handleMapping = async () => {

        const response = await mapJsonStrings(
            firstJson,
            secondJson
        );
        setResult(response);

        const allMappings = [
            ...response.mappings,

            ...response.unmappedSourcePaths.map(source => ({
                sourceField: source,
                targetField: "",
                score: null
            }))
        ];


        setMappings(allMappings);

        const allTargets = [
            ...new Set([
                ...response.mappings.map(
                    x => x.targetField
                ),
                ...response.unusedTargetPaths
            ])
        ];


        setTargetOptions(allTargets);
    };
    
    const handleSave = async () => {
        try {
            await saveMappings({
                    mappings: mappings
                        .map(x => ({
                            sourceField: x.sourceField,
                            targetField: x.targetField
                        }))
                });
            alert("Mapping został zapisany.");
            await handleLoadSavedMappings();
        } catch (error) {
            console.error(error);
            alert("Nie udało się zapisać mappingu.");
        }
    };

    const handleLoadMappings = async () => {
        const response = await getMappings();

        setMappings(response);

        const targets = [
            ...new Set(
                response.map(x => x.targetField)
            )
        ];

        setTargetOptions(targets);
    };
    
    const handleLoadSavedMappings = async () => {
        const result = await getJsonMappingsList();

        setSavedMappings(result);
    };

    const handleMappingSelect = (
        selected: JsonMappingSummary
    ) => {

        setMappings(
            selected.mappings
        );

        const targets = [
            ...new Set(
                selected.mappings
                    .map(x => x.targetField)
                    .filter(x => x !== "")
            )
        ];

        setTargetOptions(targets);
    };

    const handleMapValues = async () => {
        try {
            const mappedJson = await mapJsonValues(
                firstJson,
                secondJson,
                mappings
            );

            setMappedJson(
                JSON.stringify(mappedJson, null, 2)
            );

        } catch (error) {
            console.error(error);
            alert("Nie udało się zmapować wartości JSON.");
        }
    };
    
    return (
        <div>

            <h1>
                JSON Mapper
            </h1>
            
            <div>
                <textarea
                    placeholder="First JSON"
                    value={firstJson}
                    onChange={
                        e => setFirstJson(e.target.value)
                    }
                    rows={20}
                    cols={50}
                />
    
    
                <textarea
                    placeholder="Second JSON"
                    value={secondJson}
                    onChange={
                        e => setSecondJson(e.target.value)
                    }
                    rows={20}
                    cols={50}
                />
            </div>

            <br />


            <button onClick={handleMapping}>
                Map JSON
            </button>
            
            <MappingList mappings={savedMappings} onSelect={handleMappingSelect} />
            
            {
                mappings.length > 0 && (
                    <>
                        <MappingTable mappings={mappings} onChange={setMappings} targetOptions={targetOptions}/>
                        <div className="buttons-container">
                            <button onClick={handleSave}>
                                Save mapping
                            </button>

                            <button onClick={handleMapValues}>
                                Map json values
                            </button>
                        </div>
                        <textarea className="mapped-json-textarea"
                            placeholder="Mapped json"
                            value={mappedJson}
                            readOnly
                            rows={20}
                            cols={50}
                        />
                    </>
                )
            }

        </div>
    );
}

export default App;
