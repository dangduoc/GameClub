export abstract class PaginationRequest {
    constructor(start: number = 0, limit: number = 50) {
        this.start = start;
        this.limit = limit;
    }
    start: number;
    limit: number;
    isSortDesc?: boolean;
    sortBy?: string;
    searchBy?: string;
    searchText?: string;
}


export class PaginationResponse<T> {
    constructor(items?: T[], total?: number, start?: number, limit?: number) {
        this.items = items;
        this.total = total;
        this.start = start;
        this.limit = limit;
    }
    total?: number;
    start?: number;
    limit?: number;
    items?: T[];
}