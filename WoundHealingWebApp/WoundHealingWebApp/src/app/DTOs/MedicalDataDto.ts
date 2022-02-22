export class MedicalDataDto {
    userId: number | undefined;
    chronicDiseases!: string;
    allergies!: string;
    medication!: string;
    pregnancy!: boolean;
    tobacco!: boolean;
    alcohol!: boolean;
    drugs!: boolean;
}