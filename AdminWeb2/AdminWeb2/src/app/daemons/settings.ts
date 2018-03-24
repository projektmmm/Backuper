export interface Settings {
    DaemonId: number;
    RunAt: Date;
    Cron: string;
    BackupType: string;
    SourcePath: string;
    DestinationPath: string
}