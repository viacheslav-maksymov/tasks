import { ITaskCategory } from "./task-category.model"
import { ITaskStatus } from "./task-status.model"

export interface ITask {
    taskId?: number
    userId?: number
    title: string
    description: string
    date?: Date
    categoryId?: number
    priorityId?: number
    statusId?: string
    projectId?: number
    status?: ITaskStatus
    category?: ITaskCategory
}