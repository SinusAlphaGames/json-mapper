import {mapJsonStrings} from "./api/mappingApi.ts";
import {useState} from "react";
import MappingTable from "./components/MappingTable";
import type {Mapping, MappingResponse} from "./types/mapping.ts";

function App() {

    const [firstJson, setFirstJson] = useState("");
    const [secondJson, setSecondJson] = useState("");

    const [result, setResult] = useState<MappingResponse | null>(null);

    const [mappings, setMappings] = useState<Mapping[]>([]);
    const [targetOptions, setTargetOptions] = useState<string[]>([]);

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



    return (
        <div>

            <h1>
                JSON Mapper
            </h1>
            
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


            <br />


            <button onClick={handleMapping}>
                Map JSON
            </button>


            {
                result && (
                    <MappingTable mappings={mappings} onChange={setMappings} targetOptions={targetOptions}/>
                )
            }

        </div>
    );
}

export default App;
