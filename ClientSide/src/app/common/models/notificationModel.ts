export class NotificationModel {
    
    public message: string;
    public type: "success" | "error" | "info";
    public durationMs: number;

    constructor(
        message: string,
        type: "success" | "error" | "info",
        durationMs: number
    ) {
        this.message = message;
        this.type = type;
        this.durationMs = durationMs;
    }
    
}
