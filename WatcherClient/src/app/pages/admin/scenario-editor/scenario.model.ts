export interface Scenario {
    id?: string;
    name?: string;
    author?: string;
    duration?: number;
    countries: ScenarioCountry[];
}

export interface ScenarioCountry {
    id: string;
    name: string;
    targetScore: number;
    produce: BaselineProduce;
    targets: BaselineTargets;
}

export interface BaselineProduce {
    grain?: number;
    meat?: number;
    oil?: number;
    cocoa?: number;
    cotton?: number;
}

export interface BaselineTargets {
    grain?: number;
    meat?: number;
    energy?: number;
    chocolate?: number;
    textiles?: number;
}
