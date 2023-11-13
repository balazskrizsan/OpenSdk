import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class CompanyCreateRequest
{
    public constructor(
        private name: string,
        private domain: string,
        private companySizeId: number,
        private itSizeId: number,
    ) {
    }
}
