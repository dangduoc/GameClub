import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { catchError, map, of, throwError } from "rxjs";
import { GameClubArgument, IGameClub } from "../models/game_club";
import { PaginationRequest, PaginationResponse } from "../models/pagination_params";
import { IGameClubEvent } from "../models/game_club_event";

@Injectable({
    providedIn: 'root'
})
export class GameClubService {
    apiUrl: string = environment.url + '/api';
    constructor(private client: HttpClient) {

    }


    getClubs(args: GameClubArgument) {
        console.log(args);
        let _url = `${this.apiUrl}/clubs?start=${args.start}&limit=${args.limit}`;

        if (args.searchText) {
            _url = `${_url}&searchText=${args.searchText}`;
        }
        if (args.searchBy) {
            _url = `${_url}&searchBy=${args.searchBy}`;
        }

        return this.client.get<PaginationResponse<IGameClub>>(_url)
            .pipe(
                map((res: PaginationResponse<IGameClub>) => res)
            );
    }

    getClubById(id: string) {
        return this.client.get<IGameClub>(`${this.apiUrl}/clubs/${id}`)
            .pipe(
                catchError(
                    (error) => {
                        if (error instanceof HttpErrorResponse) {
                            return throwError(() => error);
                        }
                        else {
                            return throwError(() => new Error());
                        }
                    }),
            );
    }

    createClub(model: any) {
        return this.client.post<IGameClub>(`${this.apiUrl}/clubs`, model)
            .pipe(
                catchError(
                    (error) => {

                        if (error instanceof HttpErrorResponse) {
                            return throwError(() => error);
                        }
                        else {
                            return throwError(() => new Error());
                        }
                    }),
            );
    }

    getClubEvents(clubId: string, args: PaginationRequest) {
        return this.client.get<PaginationResponse<IGameClubEvent>>(`${this.apiUrl}/clubs/${clubId}/events?start=${args.start}&limit=${args.limit}`);
    }
    createClubEvent(clubId: string, model: any) {
        return this.client.post<IGameClubEvent>(`${this.apiUrl}/clubs/${clubId}/events`, model);
    }

}
