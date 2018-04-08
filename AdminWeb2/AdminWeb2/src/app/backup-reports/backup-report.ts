export interface BackupReport {
    Date: Date;
    Type: String;
    Size: number;

    Id: number;
    ConnectedTo: number;
    DaemonId: number;
    UserId: number;
    BackupReportId: number;
}