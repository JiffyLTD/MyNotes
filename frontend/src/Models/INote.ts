export interface INote {
    id: string;
    title: string;
    content: string;
    isFavorite: boolean;
    isDeleted: boolean;
    createdAt: Date;
    updatedAt: Date;
    imageName?: string;
}