export interface Settings {
    DaemonId: number;
    UserId: number;
    RunAt: Date;
    Cron: string;
    BackupType: string;
    SourcePath: string;
    DestinationPath: string

    Id: number;
}