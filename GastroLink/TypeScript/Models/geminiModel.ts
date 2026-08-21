
export interface GeminiApiResponse {
    candidates: {
        content: {
            parts: { text: string }[];
        }
    }[];
    error?: {
        message: string;
        code: number;
    };
}

