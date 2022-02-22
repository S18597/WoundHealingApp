export class MessageDto {
    chatId!: number;
    patientId!: number;
    doctorId!: number;
    patientName!: string;
    doctorName!: string;
    message!: string;
    messageDate!: Date;
    isPatientMessage!: boolean | undefined
}