export class Device{
    public id: number;
    public name: string;
    public uniqueIdentifier: string;
}

class Version {
    public id: number;
    public version: string;
}

export class DeviceSoftwareVersion {
    public id: number;
    public softwareId: number;
    public softwareName: string;
    public version: string;
    public allVersions: Version[];
}