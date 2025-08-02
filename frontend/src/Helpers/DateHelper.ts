export class DateHelper {
    /**
     * Преобразует ISO-дату в формат ДД.ММ.ГГГГ ЧЧ:ММ:СС с учётом локального часового пояса.
     * @param isoDateStr строка даты в формате ISO
     * @returns строка в формате "31.07.2025 15:05:35"
     */
    static formatToLocalDateTime(isoDateStr: Date): string {
        if (!isoDateStr) return '';

        const date = new Date(isoDateStr);
        if (isNaN(date.getTime())) return '';

        const dateStr = date.toLocaleDateString('ru-RU');
        const timeStr = date.toLocaleTimeString('ru-RU', {
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit',
        });

        return `${dateStr} ${timeStr}`;
    }
}
