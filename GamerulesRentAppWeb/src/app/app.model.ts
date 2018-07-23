export class Customer {
    id: string;
    name: string;
    lastname: string;
    address: string;
    area: string;
    phone: string;
    mobile: string;
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
    returnedDate: Date;
    id: string;
    prive: number;
    days: number;
    overdue: boolean;

    boardGames: Array<string>;
    constructor() {
        this.boardGames = new Array<string>();
    }
}

export class DashboardView {
    customers: number;
    activeOrders: number;
    completedOrders: number;
    delayedOrders: number;
    rentsForToday: Array<BoardGameRental>;
    constructor() {
        this.rentsForToday = new Array<BoardGameRental>();
    }
}
