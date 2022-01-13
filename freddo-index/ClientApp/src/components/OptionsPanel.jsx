import react, { useState } from 'react';

import {
    Grid,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    TextField,
    useMediaQuery,
    Typography
} from '@mui/material';

import {
    DesktopDatePicker,
    MobileDatePicker
} from '@mui/lab';

import {
    makeStyles
} from '@mui/styles';

const useClasses = makeStyles(theme => {
    console.log(theme);
    return ({
    line: {
        padding: '16px'
    },
    lineDark: {
        padding: '16px',
        backgroundColor: theme.palette.grey[800]
    },
    optionContainer: {
        width: '100%',
        margin: '8px'
    }
})})

export default function OptionsPanel(props) {
    const classes = useClasses();
    const showDesktop = useMediaQuery(theme => theme.breakpoints.up('sm'));

    const [date, setDate] = useState(new Date(new Date().getFullYear()), new Date(new Date().getMonth()), new Date(new Date().getDate()));
    
    console.log(date);

    return (
        <Grid
            item
            container
            xs={12}
            justifyContent="space-around"
            alignItems="center"
            className={classes.lineDark}
        >
            <Grid
                item
                container
                md={5}
                xs={12}
                justifyContent="flex-start"
            >
                <Typography>Currency</Typography>
                <FormControl 
                    color="secondary" 
                    sx={{ m: 1, minWidth: 100 }}
                    className={classes.optionContainer}
                >
                    <InputLabel variant="caption">Currency</InputLabel>
                    <Select
                        label="Currency"
                        color="secondary"
                        variant="standard"
                        autoWidth
                    >
                        <MenuItem value={10}>Ten</MenuItem>
                        <MenuItem value={20}>Twenty</MenuItem>
                        <MenuItem value={30}>Thirty</MenuItem>
                    </Select>
                </FormControl>
            </Grid>
            <Grid
                item
                container
                md={7}
                xs={12}
                justifyContent="flex-start"
            >
                <Typography variant="caption">Currency</Typography>
                <FormControl 
                    color="secondary" 
                    sx={{ m: 1, minWidth: 100 }}
                    className={classes.optionContainer}
                >
                    {showDesktop ?
                        <DesktopDatePicker
                            renderInput={params => 
                                <TextField 
                                    color="secondary" 
                                    variant="standard" 
                                    {...params} 
                                />
                            }
                            label="Test"
                        /> :
                        <MobileDatePicker
                            className={classes.optionContainer}
                            renderInput={params => 
                                <TextField 
                                    color="secondary" 
                                    variant="standard" 
                                    {...params} 
                                />
                            }
                            label="Test"
                        />}
                    </FormControl>
            </Grid>
        </Grid>
    );
}
