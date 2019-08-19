export interface ScenarioSummary {
    id: string;
    name: string;
    author: string;
    duration: number;
}

export interface GameCreationData {
    scenarioId: string;
    name: string;
}
