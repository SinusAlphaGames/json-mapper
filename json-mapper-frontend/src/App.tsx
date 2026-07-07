import {mapJsonStrings} from "./api/mappingApi.ts";
import {useState} from "react";
import MappingTable from "./components/MappingTable";
import type {MappingResponse} from "./types/mapping.ts";

function App() {

    const [firstJson, setFirstJson] = useState("");
    const [secondJson, setSecondJson] = useState("");

    const [result, setResult] =
        useState<MappingResponse | null>(null);


    const handleMapping = async () => {

        const response = await mapJsonStrings(
            firstJson,
            secondJson
        );

        // console.log(response);

        setResult(response);
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
                    <MappingTable data={result}/>
                )
            }

        </div>
    );
}

export default App;
