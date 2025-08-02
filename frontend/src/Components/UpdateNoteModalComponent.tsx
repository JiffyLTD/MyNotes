import React, {useState} from 'react';
import {Modal, Input, Form, Alert} from 'antd';
import {INote} from "../Models/INote";
import {DateHelper} from "../Helpers/DateHelper";

interface UpdateNoteModalProps {
    visible: boolean;
    onCancel: () => void;
    onUpdate: (id: string, title: string, content: string) => void;
    note?: INote;
}

const UpdateNoteModalComponent: React.FC<UpdateNoteModalProps> = ({ visible, onCancel, onUpdate, note }) => {
    const [form] = Form.useForm();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    if(note)
        form.setFieldsValue({
            title: note.title,
            content: note.content
        });

    const handleOk = () => {
        setError(null);
        form
            .validateFields()
            .then(async (values) => {
                setLoading(true);
                try {
                    onUpdate(note.id, values.title, values.content);
                    form.resetFields();
                } catch (e: any) {
                    setError(e.message || 'Не удалось обновить заметку');
                } finally {
                    setLoading(false);
                }
            })
            .catch((info) => {
                console.log('Validation Failed:', info);
            });
    };

    const handleCancel = () => {
        form.resetFields();
        onCancel();
    };

    return (
        <Modal
            title="Обновление заметки"
            open={visible}
            onOk={handleOk}
            onCancel={handleCancel}
            okText="Обновить"
            cancelText="Отмена"
            confirmLoading={loading}
        >
            {error && (
                <Alert
                    message="Ошибка"
                    description={error}
                    type="error"
                    showIcon
                    closable
                    style={{ marginBottom: 16 }}
                    onClose={() => setError(null)}
                />
            )}
            <Form layout="vertical" form={form}>
                <Form.Item
                    name="title"
                    label="Заголовок"
                    rules={[{ required: true, message: 'Введите заголовок' }]}
                >
                    <Input disabled={loading} />
                </Form.Item>
                <Form.Item
                    name="content"
                    label="Содержимое"
                    rules={[{ required: true, message: 'Введите содержимое' }]}
                >
                    <Input.TextArea rows={4} disabled={loading} />
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default UpdateNoteModalComponent;
