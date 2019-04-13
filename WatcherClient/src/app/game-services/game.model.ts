export interface Game {
    id: string;
    countries: Country[];
    duration: number;
}

export interface Country {
    id: string;
    name: string;
    targetScore: number;
    currentScore: number;
    scores: Year[];
}

export interface Year {
    chocolate: number;
    energy: number;
    grain: number;
    meat: number;
    textiles: number;
    score: number;
}
