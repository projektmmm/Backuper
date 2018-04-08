export interface Backups {
    Id: number;
    NextRun: Date;
    Cron: string;
    DaemonId: number;
    BackupType: string;
    SourcePath: string;
    DestinationPath: string;
}