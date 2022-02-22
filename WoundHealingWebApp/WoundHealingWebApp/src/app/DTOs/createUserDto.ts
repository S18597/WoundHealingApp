export class createUserDto {

    accountType: number | undefined;
    firstname: string | undefined;
    lastname: string | undefined;
    pesel: string | undefined;
    address: string | undefined;
    phoneNumber: string | undefined;
    emailAddress: string | undefined;
    dateOfBirth: Date | undefined;

    salt: string | undefined;
    hash: string | undefined;
}