import React, {useState, useEffect} from 'react';
import {Row, Col, Empty, Space, Spin} from 'antd';
import NoteCard from '../Components/NoteCard';
import {INote} from '../Models/INote';
import {IGetAllNotesResponse} from "../DTOs/IGetAllNotesResponse";
import NoteApiClient from "../Api/NoteApiClient";
import {IGetAllFavoriteNotesResponse} from "../DTOs/IGetAllFavoriteNotesResponse";
import FavoriteNoteApiClient from "../Api/FavoriteNoteApiClient";
import {ICreateNoteDto} from "../DTOs/ICreateNoteDto";
import {ICreateNoteResponse} from "../DTOs/ICreateNoteResponse";
import {IUpdateNoteDto} from "../DTOs/IUpdateNoteDto";
import {IDeleteNoteDto} from "../DTOs/IDeleteNoteDto";
import {IDeleteFavoriteNoteDto} from "../DTOs/IDeleteFavoriteNoteDto";
import {ICreateFavoriteNoteDto} from "../DTOs/ICreateFavoriteNoteDto";
import {LoadingOutlined, MehOutlined} from "@ant-design/icons";
import CreateNoteModalComponent from "../Components/CreateNoteModalComponent";
import NoteModalComponent from "../Components/NoteModalComponent";
import UpdateNoteModalComponent from "../Components/UpdateNoteModalComponent";

interface FavoritesPageProps {
    isDarkTheme: boolean;
}

const FavoritesPage: React.FC<FavoritesPageProps> = ({isDarkTheme}) => {
    const [notes, setNotes] = useState<INote[]>([]);
    const [loading, setLoading] = useState(true);
    const [noteFetchError, setNoteFetchError] = useState<string | null>(null);
    const [isCreateModalOpen, setIsCreateModalOpen] = useState<boolean>(false);
    const [isUpdateModalOpen, setIsUpdateModalOpen] = useState<boolean>(false);
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [currentNote, setCurrentNote] = useState<INote | null>(null);

    useEffect(() => {
        const fetchNotes = async () => {
            try {
                const allFavoriteNotes: IGetAllFavoriteNotesResponse = await FavoriteNoteApiClient.GetAllFavoriteNotesAsync();

                if (allFavoriteNotes.favoriteNotes.length !== 0) {
                    const updatedNotes = allFavoriteNotes.favoriteNotes.map(note => ({
                        ...note,
                        isFavorite: true
                    }));

                    setNotes(updatedNotes);
                } else {
                    setNotes([]);
                }

            } catch (error) {
                console.error('Ошибка при загрузке заметок:', error);
                setNoteFetchError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchNotes();
    }, []);

    const handleCreateNote = () => {
        setIsCreateModalOpen(true);
    };

    const handleCreate = async (title: string, content: string) => {
        let dto: ICreateNoteDto = {
            title: title,
            content: content
        };

        try {
            const response: ICreateNoteResponse = await NoteApiClient.CreateNoteAsync(dto);

            if (response.note) {
                const newArray: INote[] = [...notes, response.note];
                setNotes(newArray);
                setIsCreateModalOpen(false);
            } else {
                throw new Error('Не удалось создать заметку. Пожалуйста попробуйте позже.');
            }
        } catch (error: any) {
            throw new Error(error?.message || 'Произошла ошибка при создании заметки.');
        }
    };

    const handleCancel = () => {
        if (isCreateModalOpen)
            setIsCreateModalOpen(false);
        if (isModalOpen) {
            setCurrentNote(null)
            setIsModalOpen(false);
        }
        if (isUpdateModalOpen)
            setIsUpdateModalOpen(false);
    };

    const handleUpdateNote = (id: string) => {
        setIsUpdateModalOpen(true);
        setCurrentNote(notes.find(note => note.id === id));
    };

    const handleEditNote = async (id: string, title: string, content: string) => {
        let dto: IUpdateNoteDto = {
            noteId: id,
            title: title,
            content: content
        };

        try {
            const response: boolean = await NoteApiClient.UpdateNoteAsync(dto);

            if (response) {
                const updatedNotes: INote[] = notes.map(note =>
                    note.id === id
                        ? ({
                            ...note,
                            title,
                            content,
                            updatedAt: new Date().toISOString(),
                        } as unknown as INote)
                        : note
                );

                setNotes(updatedNotes);
                setIsUpdateModalOpen(false);
            } else {
                throw new Error('Не удалось обновить заметку. Пожалуйста попробуйте позже.');
            }
        } catch (error: any) {
            throw new Error(error?.message || 'Произошла ошибка при обновлении заметки.');
        }
    };

    const handleOpenNote = (id: string) => {
        setIsModalOpen(true);
        setCurrentNote(notes.find(note => note.id === id));
    };

    const handleDeleteNote = async (id: string) => {
        let dto: IDeleteNoteDto = {
            noteId: id
        };

        try {
            const response: boolean = await NoteApiClient.DeleteNoteAsync(dto);

            if (response) {
                setNotes(notes.filter(note => note.id !== id));
            } else {
                throw new Error('Не удалось удалить заметку. Пожалуйста попробуйте позже.');
            }

        } catch (error: any) {
            throw new Error(error?.message || 'Произошла ошибка при удалении заметки.');
        }
    };

    const onFavorite = async (id: string) => {
        try {
            let note = notes.find(note => note.id === id);


            let dto: IDeleteFavoriteNoteDto = {
                noteId: id
            };

            const response: boolean = await FavoriteNoteApiClient.DeleteFavoriteNoteAsync(dto);

            if (response) {
                setNotes(notes.filter(note => note.id !== id));
            } else {
                throw new Error('Не удалось добавить заметку в избранное. Пожалуйста попробуйте позже.');
            }
        } catch (error: any) {
            throw new Error(error?.message || 'Произошла ошибка при добавлении или удалении заметки из избранного.');
        }
    };

    if (loading) return (
        <div
            style={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100%',
                minHeight: '40vh',
                textAlign: 'center',
            }}
        >
            <Space direction="vertical" align="center">
                <Spin indicator={<LoadingOutlined style={{fontSize: 48}} spin/>}/>
                <span style={{color: isDarkTheme ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.85)'}}>
                            Загрузка заметок...
                </span>
            </Space>
        </div>);

    return (
        <div style={{padding: 24, minHeight: '60vh'}}>
            {noteFetchError ? (
                <div
                    style={{
                        display: 'flex',
                        justifyContent: 'center',
                        alignItems: 'center',
                        height: '100%',
                        minHeight: '40vh',
                        textAlign: 'center',
                    }}
                >
                    <span
                        style={{
                            color: isDarkTheme ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.85)',
                            fontSize: 24,
                            marginRight: 12,
                        }}
                    >
                    {noteFetchError}
                    </span>
                    <MehOutlined style={{fontSize: 36}}/>
                </div>
            ) : (
                <Row gutter={[16, 16]}>
                    <CreateNoteModalComponent
                        visible={isCreateModalOpen}
                        onCreate={handleCreate}
                        onCancel={handleCancel}
                    />
                    <NoteModalComponent
                        visible={isModalOpen}
                        onCancel={handleCancel}
                        note={currentNote}
                    />
                    <UpdateNoteModalComponent
                        visible={isUpdateModalOpen}
                        onCancel={handleCancel}
                        onUpdate={handleEditNote}
                        note={currentNote}
                    />
                    {notes.length > 0 ? (
                        <Row gutter={[16, 16]}>
                            {notes.map((note) => (
                                <Col key={note.id}>
                                    <NoteCard
                                        note={note}
                                        onEdit={handleUpdateNote}
                                        onOpen={handleOpenNote}
                                        onDelete={handleDeleteNote}
                                        onFavorite={onFavorite}
                                        isDarkTheme={isDarkTheme}
                                    />
                                </Col>
                            ))}
                        </Row>
                    ) : (
                        <div style={{
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center',
                            height: '60vh',
                            width: '100%',
                        }}>
                            <Empty description="Нет избранных заметок" />
                        </div>

                    )}
                </Row>
            )}
        </div>
    );
};

export default FavoritesPage;