export interface Mapping {
    sourceField: string;
    targetField: string;
    score: number | null;
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

export interface SaveMappingRequest {
    mappings: {
        sourceField: string;
        targetField: string;
    }[];
}

export interface JsonMappingSummary {
    id: string;
    createdAt: string;
    mappings: Mapping[];
}