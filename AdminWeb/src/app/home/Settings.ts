export interface Settings{
    DeamonId: number;
    RunAt: Date;
    Cron: string;
    BackupType: string;
    SourcePath: string;
    DestinationPath: string
}