import React, {useState, useEffect} from 'react';
import {Row, Col, Empty, Space, Spin} from 'antd';
import NoteCard from '../Components/NoteCard';
import {INote} from '../Models/INote';
import {LoadingOutlined, MehOutlined} from "@ant-design/icons";
import {IGetAllNotesResponse} from "../DTOs/IGetAllNotesResponse";
import NoteApiClient from "../Api/NoteApiClient";
import {IRestoreNoteDto} from "../DTOs/IRestoreNoteDto";

interface DeletedPageProps {
    isDarkTheme: boolean;
}

const DeletedPage: React.FC<DeletedPageProps> = ({isDarkTheme}) => {
    const [deletedNotes, setDeletedNotes] = useState<INote[]>([]);
    const [loading, setLoading] = useState(true);
    const [noteFetchError, setNoteFetchError] = useState<string | null>(null);

    useEffect(() => {
        const fetchDeleted = async () => {
            try {
                const response: IGetAllNotesResponse = await NoteApiClient.GetAllDeletedNotesAsync();
                setDeletedNotes(response.notes);
            } catch (error) {
                console.error('Ошибка при загрузке заметок:', error);
                setNoteFetchError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchDeleted();
    }, []);

    const handleRestoreNote = async (id: string) => {
        let dto: IRestoreNoteDto =  {
            noteId: id
        };

        try {
            const response: boolean = await NoteApiClient.RestoreNoteAsync(dto);

            if (response) {
                setDeletedNotes(deletedNotes.filter(note => note.id !== id));
            } else {
                throw new Error('Не удалось восстановить заметку. Пожалуйста попробуйте позже.');
            }

        }
        catch (error: any){
            throw new Error(error?.message || 'Произошла ошибка при восстановлении заметки.');
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
                <MehOutlined style={{ fontSize: 36 }} />
            </div>
        ) : (
            <div style={{padding: 24, minHeight: '60vh'}}>
                {deletedNotes.length > 0 ? (
                    <Row gutter={[16, 16]}>
                        {deletedNotes.map(note => (
                            <Col key={note.id}>
                                <NoteCard
                                    note={note}
                                    onRestore={handleRestoreNote}
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
                        <Empty
                            description="Корзина пуста"
                            imageStyle={{
                                height: 100,
                            }}
                        />
                    </div>
                )}
            </div>
        )}
    </div>
    );
};

export default DeletedPage;