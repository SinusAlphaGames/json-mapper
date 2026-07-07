import type {Mapping} from "../types/mapping";


interface Props {
    mappings: Mapping[];
    onChange: (
        mappings: Mapping[]
    ) => void;
    targetOptions: string[];
}


function MappingTable({ mappings, onChange, targetOptions }: Props) {

    const handleTargetChange = (
        sourceField: string,
        newTarget: string
    ) => {

        const updated = mappings.map(mapping => {
            if (mapping.sourceField === sourceField) {
                return {
                        ...mapping,
                        targetField: newTarget,
                        score: null
                };
            }

            if (
                newTarget !== "" && mapping.targetField === newTarget
            ) {
                return {
                    ...mapping,
                    targetField: "",
                    score: null
                };
            }
            
            return mapping;
        });
        
        onChange(updated);
    };

    function getConfidenceClass(mapping: Mapping) {

        if (mapping.score === null) {
            return "";
        }

        if (mapping.sourceField === mapping.targetField) {
            return "exact-match";
        }

        return "mapped-match";
    }

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
                mappings.map((mapping, index) => (

                    <tr key={index} className={getConfidenceClass(mapping)}>

                        <td>
                            {mapping.sourceField}
                        </td>

                        <td>
                            <select
                                value={mapping.targetField}
                                onChange={(e) =>
                                    handleTargetChange(
                                        mapping.sourceField,
                                        e.target.value
                                    )
                                }
                            >
                                <option value="">
                                    -- no mapping --
                                </option>
                                {targetOptions.map(target => (
                                    <option key={target} value={target}>
                                        {target}
                                    </option>
                                ))}
                            </select>
                        </td>

                        <td>
                            {
                                mapping.score !== null
                                    ? `${(mapping.score * 100).toFixed(1)}%`
                                    : "manual"
                            }
                        </td>

                    </tr>

                ))
            }

            </tbody>

        </table>
    );
}


export default MappingTable;