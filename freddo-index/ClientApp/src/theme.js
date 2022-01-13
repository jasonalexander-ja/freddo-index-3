import { 
    createTheme 
} from '@mui/material/styles';
import {
    createTheme as createLegacyTheme
} from '@material-ui/core/styles'
import {
    deepPurple,
    yellow
} from '@mui/material/colors';

const useTheme = () => {
    return createTheme({
        palette: {
            mode: 'dark',
            primary: deepPurple,
            secondary: yellow
        }
    });
};

export const useLegacyTheme = () => {
    return createLegacyTheme({
        palette: {
            type: 'dark',
            primary: deepPurple,
            secondary: yellow
        }
    });
}

export default useTheme;
