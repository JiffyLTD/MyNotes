import axios, { AxiosInstance, AxiosResponse } from 'axios';
import {IGetAllNotesResponse} from "../DTOs/IGetAllNotesResponse";
import {ICreateNoteDto} from "../DTOs/ICreateNoteDto";
import {ICreateNoteResponse} from "../DTOs/ICreateNoteResponse";
import {IGetAllDeletedNotesResponse} from "../DTOs/IGetAllDeletedNotesResponse";
import {IDeleteNoteDto} from "../DTOs/IDeleteNoteDto";
import {IUpdateNoteDto} from "../DTOs/IUpdateNoteDto";
import {IGetAllFavoriteNotesResponse} from "../DTOs/IGetAllFavoriteNotesResponse";
import {ICreateFavoriteNoteDto} from "../DTOs/ICreateFavoriteNoteDto";
import {IDeleteFavoriteNoteDto} from "../DTOs/IDeleteFavoriteNoteDto";

export default class FavoriteNoteApiClient {
    private static axiosInstance: AxiosInstance = axios.create({
        baseURL: 'http://localhost:5100/api',
        timeout: 5000,
        headers: {
            'Content-Type': 'application/json',
        },
    });

    static SetAccessToken(token: string | null) {
        this.axiosInstance.defaults.headers.common['Authorization'] = token ? `Bearer ${token}` : '';
    }

    static async GetAllFavoriteNotesAsync<T = IGetAllFavoriteNotesResponse>(): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.get("/favorites");
        return response.data;
    }

    static async CreateFavoriteNoteAsync<T = boolean>(data: ICreateFavoriteNoteDto): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.post("/favorite", data);
        return response.data;
    }

    static async DeleteFavoriteNoteAsync<T = boolean>(data: IDeleteFavoriteNoteDto): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.delete("/favorite", { data });
        return response.data;
    }
}
