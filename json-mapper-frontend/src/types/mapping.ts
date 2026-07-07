export interface Mapping {
    sourceField: string;
    targetField: string;
    score: number;
}

export interface MappingResponse {
    mappings: Mapping[];
    unmappedSourcePaths: string[];
    unusedTargetPaths: string[];
}

export interface MappingRequest {
    firstJson: unknown;
    secondJson: unknown;
}