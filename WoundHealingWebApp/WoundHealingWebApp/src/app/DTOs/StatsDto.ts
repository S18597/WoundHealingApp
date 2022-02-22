import { WoundTypesStats } from "./WoundTypesStats";

export class StatsDto {
    doctorId!: number;
    patientsCnt!: number;
    finishedTreatmentsCnt!: number;
    appointmentsCnt!: number;
    woundsCnt!: number;
    avgTreatmentDays!: number;
    woundTypesStats!: WoundTypesStats[];
}