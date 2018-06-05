export interface Settings {
    DaemonId: number;
    Cron: string;
    BackupType: string;
    SourcePath: string;
    DestinationPath: string;
    FTPServerAdress: string;
    FTPPort: string;
    FTPUsername: string;
    FTPPassword: string;
    SSHServerAdress: string;
    SSHPort: string;
    SSHUsername: string;
    SSHPassword: string;
    Override: Boolean;
    Rar: Boolean;
    //Batches: Boolean;
    BatchBeforePath: string;
    BatchAfterPath: string;
    BatchBeforeCode: string;
    BatchAfterCode: string;
    Id: number;
}