import { IRole } from "src/app/user/models/role.model"

export interface IUser {
    userId: number
    userName: string
    email: string
    roles: IRole[]
}