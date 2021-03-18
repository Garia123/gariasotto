export class ReserveDescription {
    state: ReserveState;
    description: string;
}

enum ReserveState {
    CREATED,
    PAYMENT_APPROVAL,
    APPROVED,
    REJECTED,
    EXPIRED
}