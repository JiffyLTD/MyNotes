import React from 'react';
import { Modal, Input, Form } from 'antd';
import {INote} from "../Models/INote";
import {DateHelper} from "../Helpers/DateHelper";

interface NoteModalProps {
    visible: boolean;
    onCancel: () => void;
    note?: INote;
}

const NoteModalComponent: React.FC<NoteModalProps> = ({ visible, onCancel, note }) => {
    const [form] = Form.useForm();

    if(note)
        form.setFieldsValue({
            title: note.title,
            content: note.content,
            createdAt: DateHelper.formatToLocalDateTime(note.createdAt),
            updatedAt: DateHelper.formatToLocalDateTime(note.updatedAt)
        });

    const handleCancel = () => {
        form.resetFields();
        onCancel();
    };

    return (
        <Modal
            title="Ваша заметка"
            open={visible}
            onCancel={handleCancel}
            cancelText="Отмена"
            footer={null}
        >
            <Form layout="vertical" form={form}>
                <Form.Item name="title" label="Заголовок">
                    <Input disabled style={{ color: 'white', backgroundColor: '#1f1f1f' }} />
                </Form.Item>
                <Form.Item name="content" label="Содержимое">
                    <Input.TextArea rows={4} disabled style={{ color: 'white', backgroundColor: '#1f1f1f' }} />
                </Form.Item>
                <Form.Item name="createdAt" label="Дата создания">
                    <Input disabled style={{ color: 'white', backgroundColor: '#1f1f1f' }} />
                </Form.Item>
                <Form.Item name="updatedAt" label="Дата последнего обновления">
                    <Input disabled style={{ color: 'white', backgroundColor: '#1f1f1f' }} />
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default NoteModalComponent;
