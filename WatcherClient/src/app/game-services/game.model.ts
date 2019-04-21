export interface Game {
    id: string;
    duration: number;
    currentYear: number;
    name: string;
    countries: Country[];
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
