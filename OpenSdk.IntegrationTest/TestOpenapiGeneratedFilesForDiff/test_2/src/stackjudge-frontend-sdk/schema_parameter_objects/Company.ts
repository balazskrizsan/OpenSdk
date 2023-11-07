import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class Company
{
    public constructor(
        private id: number,
        private name: string,
        private domain: string,
        private companySizeId: number,
        private itSizeId: number,
        private logoPath: string,
        private createdAt: string,
        private createdBy: string,
    ) {
    }
}
