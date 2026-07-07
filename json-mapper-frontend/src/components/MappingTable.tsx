import type { MappingResponse } from "../types/mapping";


interface Props {
    data: MappingResponse;
}


function MappingTable({ data }: Props) {

    return (
        <table>
            <thead>
            <tr>
                <th>
                    Source
                </th>

                <th>
                    Target
                </th>

                <th>
                    Confidence
                </th>
            </tr>
            </thead>


            <tbody>

            {
                data.mappings.map((mapping, index) => (

                    <tr key={index}>

                        <td>
                            {mapping.sourceField}
                        </td>

                        <td>
                            {mapping.targetField}
                        </td>

                        <td>
                            {
                                (mapping.score * 100)
                                    .toFixed(1)
                            }%
                        </td>

                    </tr>

                ))
            }


            {
                data.unmappedSourcePaths.map(
                    (source, index) => (

                        <tr key={`unmapped-${index}`}>

                            <td>
                                {source}
                            </td>

                            <td>
                                -
                            </td>

                            <td>
                                -
                            </td>

                        </tr>

                    )
                )
            }

            </tbody>

        </table>
    );
}


export default MappingTable;