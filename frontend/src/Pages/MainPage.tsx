import React, {useState, useEffect} from 'react';
import { Row, Col, Spin, Space } from 'antd';
import NoteCard from '../Components/NoteCard';
import {INote} from '../Models/INote';
import NoteApiClient from "../Api/NoteApiClient";
import {LoadingOutlined, MehOutlined} from '@ant-design/icons';
import {IGetAllNotesResponse} from "../DTOs/IGetAllNotesResponse";
import CreateNoteModalComponent from "../Components/CreateNoteModalComponent";
import {ICreateNoteDto} from "../DTOs/ICreateNoteDto";
import {ICreateNoteResponse} from "../DTOs/ICreateNoteResponse";
import {IDeleteNoteDto} from "../DTOs/IDeleteNoteDto";
import NoteModalComponent from "../Components/NoteModalComponent";
import {IUpdateNoteDto} from "../DTOs/IUpdateNoteDto";
import UpdateNoteModalComponent from "../Components/UpdateNoteModalComponent";
import {IGetAllFavoriteNotesResponse} from "../DTOs/IGetAllFavoriteNotesResponse";
import FavoriteNoteApiClient from "../Api/FavoriteNoteApiClient";
import {IDeleteFavoriteNoteDto} from "../DTOs/IDeleteFavoriteNoteDto";
import {ICreateFavoriteNoteDto} from "../DTOs/ICreateFavoriteNoteDto";

interface MainPageProps {
    isDarkTheme: boolean;
}

const MainPage: React.FC<MainPageProps> = ({isDarkTheme}) => {
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
                const allNotes: IGetAllNotesResponse = await NoteApiClient.GetAllNotesAsync();
                const allFavoriteNotes: IGetAllFavoriteNotesResponse = await FavoriteNoteApiClient.GetAllFavoriteNotesAsync();

                if(allFavoriteNotes.favoriteNotes.length !== 0) {
                    const favoriteNoteIds = new Set(allFavoriteNotes.favoriteNotes.map(note => note.id));

                    const updatedNotes = allNotes.notes.map(note => ({
                        ...note,
                        isFavorite: favoriteNoteIds.has(note.id)
                    }));

                    setNotes(updatedNotes);
                }else{
                    setNotes(allNotes.notes);
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
        let dto: ICreateNoteDto =  {
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
        if(isCreateModalOpen)
            setIsCreateModalOpen(false);
        if(isModalOpen) {
            setCurrentNote(null)
            setIsModalOpen(false);
        }
        if(isUpdateModalOpen)
            setIsUpdateModalOpen(false);
    };

    const handleUpdateNote = (id: string) => {
        setIsUpdateModalOpen(true);
        setCurrentNote(notes.find(note => note.id === id));
    };

    const handleEditNote = async (id: string, title: string, content: string) => {
        let dto: IUpdateNoteDto =  {
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
            }
            else {
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
        let dto: IDeleteNoteDto =  {
            noteId: id
        };

        try {
            const response: boolean = await NoteApiClient.DeleteNoteAsync(dto);

            if (response) {
                setNotes(notes.filter(note => note.id !== id));
            } else {
                throw new Error('Не удалось удалить заметку. Пожалуйста попробуйте позже.');
            }

        }
        catch (error: any){
            throw new Error(error?.message || 'Произошла ошибка при удалении заметки.');
        }
    };

    const onFavorite = async (id: string) => {
        try {
            let note = notes.find(note => note.id === id);

            if(note.isFavorite){
                let dto: IDeleteFavoriteNoteDto = {
                    noteId: id
                };

                const response: boolean = await FavoriteNoteApiClient.DeleteFavoriteNoteAsync(dto);

                if (response) {
                    const updatedNotes: INote[] = notes.map(note =>
                        note.id === id
                            ? ({
                                ...note,
                                isFavorite: false,
                                updatedAt: new Date().toISOString()
                            } as unknown as INote)
                            : note
                    );

                    setNotes(updatedNotes);
                } else {
                    throw new Error('Не удалось добавить заметку в избранное. Пожалуйста попробуйте позже.');
                }
            }else{
                let dto: ICreateFavoriteNoteDto = {
                    noteId: id
                };

                const response: boolean = await FavoriteNoteApiClient.CreateFavoriteNoteAsync(dto);

                if (response) {
                    const updatedNotes: INote[] = notes.map(note =>
                        note.id === id
                            ? ({
                                ...note,
                                isFavorite: true,
                                updatedAt: new Date().toISOString()
                            } as unknown as INote)
                            : note
                    );

                    setNotes(updatedNotes);
                } else {
                    throw new Error('Не удалось добавить заметку в избранное. Пожалуйста попробуйте позже.');
                }
            }
        }
        catch (error: any){
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
        <div style={{ padding: 24, minHeight: '60vh' }}>
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
                    <MehOutlined style={{ fontSize: 36 }} />
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
                    <Col >
                        <NoteCard isNew onCreate={handleCreateNote} isDarkTheme={isDarkTheme} />
                    </Col>
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
            )}
        </div>
    );
};

export default MainPage;