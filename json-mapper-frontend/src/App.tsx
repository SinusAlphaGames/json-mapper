import {mapJsonStrings} from "./api/mappingApi.ts";

function App() {
    const testMapping = async () => {

        const firstJson = `
        {
            "id": 1,
            "name": "Anna"
        }
        `;

        const secondJson = `
        {
            "userId": 2,
            "fullName": "Jan"
        }
        `;


        const response = await mapJsonStrings(
            firstJson,
            secondJson
        );


        console.log(response);
    };


    return (
        <div>
            <h1>JSON Mapper</h1>

            <button onClick={testMapping}>
                Test mapping
            </button>
        </div>
    );
}

export default App;
