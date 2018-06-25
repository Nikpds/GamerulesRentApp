export class Customer {
    id: string;
    name: string;
    lastname: string;
    address: string;
    area: string;
    phone: string;
    mobile: string;
    cardNumber: string;
    postalCode: string;
    identityNo: string;
    isVerified: boolean;
    created: Date;
}

export class BoardGameRental {
    customerId: string;
    customer: Customer;
    created: Date;
    rentDate: Date;
    returnDate: Date;

    prive: number;
    days: number;
    overdue: boolean;
    
    boardGames: Array<string>;
    constructor() {
        this.boardGames = new Array<string>();
    }
}
