import { IBaseEntity } from "./base_entity";
import { PaginationRequest } from "./pagination_params";

export interface IGameClubEvent extends IBaseEntity {


    title: string;
    clubId: number;
    scheduledAt: Date;
    description?: string;
    createdAt?: Date;
    updatedAt?: Date;
}
export class GameClubEventArgument extends PaginationRequest {
    constructor(start = 0, limit = 50) {
        super(start, limit);
    }
}