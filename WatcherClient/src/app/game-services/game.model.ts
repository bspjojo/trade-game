export interface Game {
    id: string;
    countries: Country[];
}

export interface Country {
    id: string;
    name: string;
    targetScore: number;
    scores: Year[];
}

export interface Year {
    chocolate: number;
    energy: number;
    grain: number;
    meat: number;
    textiles: number;
}
