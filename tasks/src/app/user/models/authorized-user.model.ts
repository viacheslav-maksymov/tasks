import { IRole } from "./role.model";

export interface IAuthorizedUser {
    userName: string,
    roles: IRole[]
}