import axios, { AxiosInstance, AxiosResponse } from 'axios';
import {IGetAllNotesResponse} from "../DTOs/IGetAllNotesResponse";
import {ICreateNoteDto} from "../DTOs/ICreateNoteDto";
import {ICreateNoteResponse} from "../DTOs/ICreateNoteResponse";
import {IGetAllDeletedNotesResponse} from "../DTOs/IGetAllDeletedNotesResponse";
import {IDeleteNoteDto} from "../DTOs/IDeleteNoteDto";
import {IUpdateNoteDto} from "../DTOs/IUpdateNoteDto";

export default class NoteApiClient {
    private static axiosInstance: AxiosInstance = axios.create({
        baseURL: 'https://localhost:5000/api',
        timeout: 5000,
        headers: {
            'Content-Type': 'application/json',
        },
    });

    static SetAccessToken(token: string | null) {
        this.axiosInstance.defaults.headers.common['Authorization'] = token ? `Bearer ${token}` : '';
    }

    static async GetAllNotesAsync<T = IGetAllNotesResponse>(): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.get("/notes");
        return response.data;
    }

    static async CreateNoteAsync<T = ICreateNoteResponse>(data: ICreateNoteDto): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.post("/note", data);
        return response.data;
    }

    static async UpdateNoteAsync<T = boolean>(data: IUpdateNoteDto): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.put("/note", data);
        return response.data;
    }

    static async GetAllDeletedNotesAsync<T = IGetAllDeletedNotesResponse>(): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.get("/notes/deleted");
        return response.data;
    }

    static async DeleteNoteAsync<T = boolean>(data: IDeleteNoteDto): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.delete("/note", { data });
        return response.data;
    }

    static async RestoreNoteAsync<T = boolean>(data: IDeleteNoteDto): Promise<T> {
        const response: AxiosResponse<T> = await this.axiosInstance.post("/note/restore", data);
        return response.data;
    }
}
