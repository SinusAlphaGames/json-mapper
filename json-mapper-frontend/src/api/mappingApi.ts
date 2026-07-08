import type {
    MappingRequest,
    MappingResponse, SaveMappingRequest,
} from "../types/mapping";

const API_URL = "http://localhost:5217/api/mapping/json";

export async function mapJsons(
    request: MappingRequest
): Promise<MappingResponse> {

    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(request),
    });

    if (!response.ok) {
        throw new Error(
            `Backend returned ${response.status}`
        );
    }

    return response.json();
}

export async function saveMappings(
    request: SaveMappingRequest
): Promise<void> {

    const response = await fetch(API_URL + "/save", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(request),
    });

    if (!response.ok) {
        throw new Error(
            `Backend returned ${response.status}`
        );
    }
}


export async function mapJsonStrings(
    first: string,
    second: string
): Promise<MappingResponse> {

    return mapJsons({
        firstJson: JSON.parse(first),
        secondJson: JSON.parse(second),
    });
}
