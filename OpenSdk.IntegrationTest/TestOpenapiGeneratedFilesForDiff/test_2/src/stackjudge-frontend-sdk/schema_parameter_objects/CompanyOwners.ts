import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class CompanyOwners
{
    public constructor(
        private companyId: number,
        private owners: string[],
    ) {
    }
}
