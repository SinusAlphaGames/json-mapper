import type { JsonMappingSummary } from "../types/mapping";

interface Props {
    mappings: JsonMappingSummary[];
    onSelect: (mapping: JsonMappingSummary) => void;
}

function MappingList({ mappings, onSelect }: Props) {

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
                    <tr key={mapping.id}
                        onClick={() => onSelect(mapping)}
                        style={{
                            cursor: "pointer"
                        }}>

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