export interface Settings {
    Id: number;

    DaemonId: number;
    RunAt: Date;
    Cron: string;
    BackupType: string;
    SourcePath: string;
    DestinationPath: string
}