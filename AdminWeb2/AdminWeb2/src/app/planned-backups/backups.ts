export interface Backups {
    Id: number;
    RunAt: Date;
    Cron: string;
    DaemonId: number;
    BackupType: string;
    SourcePath: string;
    DestinationPath: string;
}