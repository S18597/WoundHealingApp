import { MyAppointmentDto } from "./MyAppointmentDto";

export class WoundAppointmentDto {
    woundId!: number;
    woundType!: string;
    woundRegisterDate!: Date;
    appointment!: MyAppointmentDto;
}