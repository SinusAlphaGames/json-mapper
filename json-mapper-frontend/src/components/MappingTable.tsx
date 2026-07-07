import type {Mapping, MappingResponse} from "../types/mapping";
import {useState} from "react";


interface Props {
    data: MappingResponse;
}


function MappingTable({ data }: Props) {
    const initialMappings: Mapping[] = [
        ...data.mappings,

        ...data.unmappedSourcePaths.map(source => ({
            sourceField: source,
            targetField: "",
            score: null
        }))
    ];
    
    
    const [mappings, setMappings] = useState<Mapping[]>(
        initialMappings
    );

    const targetOptions = [
        ...new Set([
            ...data.mappings.map(x => x.targetField),
            ...data.unusedTargetPaths
        ])
    ];

    const handleTargetChange = (
        sourceField: string,
        newTarget: string
    ) => {

        setMappings(current =>
            current.map(mapping => {

                if (mapping.sourceField === sourceField) {
                    return {
                        ...mapping,
                        targetField: newTarget,
                    };
                }

                return mapping;
            })
        );
    };


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

                    <tr key={index}>

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