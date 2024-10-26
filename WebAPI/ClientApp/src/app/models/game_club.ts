import { IBaseEntity } from "./base_entity";
import { PaginationRequest } from "./pagination_params";

export interface IGameClub extends IBaseEntity {
    name: string;
    description?: string;
    createdAt?: Date;
    updatedAt?: Date;
}

export class GameClubArgument extends PaginationRequest {
    constructor(start = 0, limit = 50) {
        super(start, limit);
    }
}