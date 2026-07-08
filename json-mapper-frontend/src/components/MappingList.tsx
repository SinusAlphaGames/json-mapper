import type { JsonMappingSummary } from "../types/mapping";

interface Props {
    mappings: JsonMappingSummary[];
}

function MappingList({ mappings }: Props) {

    return (
        <table>
            <thead>
            <tr>
                <th>
                    Id
                </th>

                <th>
                    Created At
                </th>

                <th>
                    Fields
                </th>
            </tr>
            </thead>

            <tbody>
            {
                mappings.map(mapping => (
                    <tr key={mapping.id}>

                        <td>
                            {mapping.id}
                        </td>

                        <td>
                            {new Date(
                                mapping.createdAt
                            ).toLocaleString()}
                        </td>

                        <td>
                            {mapping.mappings.length}
                        </td>

                    </tr>
                ))
            }
            </tbody>
        </table>
    );
}

export default MappingList;