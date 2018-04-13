
export interface Backups {
    Id: number;
    UserId: number;
    NextRun: Date;
    Cron: string;
    DaemonId: number;
    BackupType: string;
    SourcePath: string;
    DestinationPath: string;
}

export interface ErrorDetails {
    Id: number;
    AffectedFiles: number;
    Problem: string;
    BackupId: number;
    DaemonId: number;
    DaemonName: string;
    Solved: boolean;
}