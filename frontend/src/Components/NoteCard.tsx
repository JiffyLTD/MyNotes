import React from 'react';
import {Card, Popconfirm} from 'antd';
import {
    EditOutlined,
    DeleteOutlined,
    SearchOutlined,
    ReloadOutlined
} from '@ant-design/icons';
import { INote } from '../Models/INote';
import {Meta} from "antd/es/list/Item";
import { Typography } from 'antd';

const { Text } = Typography;

interface NoteCardProps {
    note?: INote;
    isNew?: boolean;
    onCreate?: () => void;
    onEdit?: (id: string, title: string, content: string) => void;
    onOpen?: (id: string) => void;
    onDelete?: (id: string) => void;
    onRestore?: (id: string) => void;
    isDarkTheme: boolean;
}

const NoteCard: React.FC<NoteCardProps> = ({
                                               note,
                                               isNew = false,
                                               onCreate,
                                               onEdit,
                                               onOpen,
                                               onDelete,
                                               onRestore,
                                               isDarkTheme
                                           }) => {

    const truncate = (text: string, maxLength: number): string => {
        return text.length > maxLength ? text.slice(0, maxLength) + '...' : text;
    };

    if (isNew) {
        return (
        <Card
            style={{ width: 300 }}
            cover={
                <img
                    alt="example"
                    src="/defaultCardImage.jpg"
                />
            }
            actions={[
                "Создать новую заметку"
            ]}
            onClick={() => onCreate()}
        >
            <Meta
                title="Пример заголовка заметки"
                description={
                    <>
                        <div style={{ marginBottom: 8 }}>Пример содержания заметки</div>
                        <Text type="secondary" style={{ fontSize: 12 }}>
                            Обновлено: {new Date().toLocaleString()}
                        </Text>
                    </>
                }
            />
        </Card>
        );
    }

    return (
        <Card
            style={{ width: 300 }}
            cover={
                <img
                    alt="example"
                    src="/defaultCardImage.jpg"
                />
            }
            actions={
                onRestore
                    ? [
                        <ReloadOutlined key="restore" onClick={() => onRestore?.(note!.id)} />
                    ]
                    : [
                        <SearchOutlined key="open" onClick={() => onOpen?.(note!.id)} />,
                        <EditOutlined key="edit" onClick={() => onEdit?.(note!.id, note!.title, note!.content)} />,
                        <Popconfirm
                            key="delete"
                            title="Удалить заметку?"
                            description="Вы уверены, что хотите удалить эту заметку? Это действие необратимо."
                            onConfirm={() => onDelete?.(note!.id)}
                            okText="Да"
                            cancelText="Отмена"
                        >
                            <DeleteOutlined />
                        </Popconfirm>
                    ]
            }
        >
            <Meta
                title={truncate(note!.title, 24)} // например, 40 символов

                description={
                    <>
                        <div style={{ marginBottom: 8 }}>{truncate(note!.content, 24)}</div>
                        <Text type="secondary" style={{ fontSize: 12 }}>
                            Обновлено: {new Date(note!.updatedAt).toLocaleString()}
                        </Text>
                    </>
                }
            />
        </Card>
    );
};

export default NoteCard;