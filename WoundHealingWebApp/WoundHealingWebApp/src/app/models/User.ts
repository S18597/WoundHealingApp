export class User {
    
    userId: number | undefined;
    firstname: string | undefined;
    lastname: string | undefined;
    pesel: string | undefined;
    address: string | undefined;
    phoneNumber: string | undefined;
    emailAddress!: string;
    dateOfBirth: Date | undefined;
    isPatient: boolean | undefined;
    isDoctor: boolean | undefined;
}